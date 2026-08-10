using STAF.Playwright.Framework.Accessibility;
using STAF.Playwright.Framework.AI;
using STAF.Playwright.Framework.Mcp;

namespace STAF.Playwright.Tests.Tests.Framework
{
    /// <summary>
    /// Verifies STAF 3.0 Phase 2 extension stubs (AI, MCP, accessibility) are usable.
    /// </summary>
    [TestClass]
    public class Phase2HooksTests
    {
        [TestMethod]
        public async Task NullAIProvider_CompleteAsync_ReturnsEmpty()
        {
            IAIProvider provider = new NullAIProvider();
            Assert.AreEqual("null", provider.Name);
            string result = await provider.CompleteAsync("hello");
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public async Task McpToolRegistry_Invoke_RequiresGrantedPermission()
        {
            var registry = new McpToolRegistry { GrantedPermissions = McpPermission.ReadOnly };
            registry.Register(new StubMcpTool("echo", McpPermission.ReadOnly, "ok"));
            registry.Register(new StubMcpTool("danger", McpPermission.Execute, "nope"));

            Assert.IsTrue(registry.TryGet("echo", out _));
            string echo = await registry.InvokeAsync("echo", new Dictionary<string, string>());
            Assert.AreEqual("ok", echo);

            await Assert.ThrowsExactlyAsync<UnauthorizedAccessException>(
                () => registry.InvokeAsync("danger", new Dictionary<string, string>()));

            await Assert.ThrowsExactlyAsync<InvalidOperationException>(
                () => registry.InvokeAsync("missing", new Dictionary<string, string>()));
        }

        [TestMethod]
        public async Task NullAccessibilityScanner_ScanAsync_ReturnsEmpty()
        {
            IAccessibilityScanner scanner = new NullAccessibilityScanner();
            var violations = await scanner.ScanAsync(page: null!);
            Assert.AreEqual(0, violations.Count);
        }

        private sealed class StubMcpTool : IMcpTool
        {
            private readonly string _result;

            public StubMcpTool(string name, McpPermission permission, string result)
            {
                Name = name;
                RequiredPermission = permission;
                _result = result;
            }

            public string Name { get; }
            public McpPermission RequiredPermission { get; }

            public Task<string> InvokeAsync(IReadOnlyDictionary<string, string> arguments, CancellationToken cancellationToken = default)
                => Task.FromResult(_result);
        }
    }
}
