# Migration plan: Windows Event Log (System.Diagnostics.EventLog) -> OpenTelemetry on Azure

Technology X: Windows Event Log (`System.Diagnostics.EventLog`)
Technology Y: OpenTelemetry on Azure
Knowledge base: `#open_telemetry_on_azure_knowledge_base`

Summary of analysis
- Repository scan (semantic + literal) did not find any usages of `System.Diagnostics.EventLog` or `EventLog.WriteEntry`.
- Current project: .NET 8 console application (C# 12). Project uses `Program.cs` with a plain `Main` method and no DI host.
- Even though `EventLog` is not present, the migration plan will prepare the codebase to use OpenTelemetry (traces/logs/metrics) and to replace any future `EventLog` usages with OpenTelemetry APIs.

Goals
- Add OpenTelemetry + Azure Monitor packages and minimal initialization so the app can emit traces and logs to Azure Monitor.
- Provide a small, centralized telemetry initializer that is easy to call from existing code (no invasive business logic changes).
- Keep project target framework (NET 8) unchanged.

Packages (recommended from knowledge base)
- `Azure.Monitor.OpenTelemetry.Exporter` 1.4.0
- `Azure.Monitor.OpenTelemetry.AspNetCore` 1.3.0 (only if hosting via Generic Host; optional)
- `OpenTelemetry.Extensions.Hosting` 1.12.0
- `OpenTelemetry.Instrumentation.Http` 1.12.0
- `OpenTelemetry.Instrumentation.SqlClient` 1.12.0-beta.2 (optional if using SqlClient)
- `Azure.Identity` 1.14.0

Add packages (for SDK-style projects):
- `dotnet add <Project>.csproj package Azure.Monitor.OpenTelemetry.Exporter --version 1.4.0`
- `dotnet add <Project>.csproj package OpenTelemetry.Extensions.Hosting --version 1.12.0`
- `dotnet add <Project>.csproj package OpenTelemetry.Instrumentation.Http --version 1.12.0`
- `dotnet add <Project>.csproj package Azure.Identity --version 1.14.0`

Planned code changes (minimal, compile-focused)
1. Add a new helper to initialize telemetry:
   - File: `Presentation/Telemetry/TelemetryInitializer.cs`
   - Purpose: expose a method `TelemetryInitializer.Initialize(string? connectionString = null)` that configures `ActivitySource`, `Meter`, and `LoggerFactory` (or `HostBuilder` when appropriate). It should be non-invasive and safe to call from `Program.Main`.
2. Update `Program.cs` to call the initializer at startup (before any business operations):
   - Import the Telemetry initializer and call `TelemetryInitializer.Initialize(...)`.
   - Optionally create a `LoggerFactory`/`ILogger` and pass to other classes if needed.
3. Replace future `EventLog.WriteEntry` usages (if any) with `ILogger` or Activity events. Since no `EventLog` calls exist now, include a short developer note in `TelemetryInitializer` showing how to convert `EventLog.WriteEntry(...)` to a structured log or activity event.

Files likely to be added/edited
- Edit: `Program.cs` (add using and call to initializer)
- Add: `Presentation/Telemetry/TelemetryInitializer.cs`
- (Optional) Add: `Presentation/Telemetry/TelemetryExtensions.md` with examples for converting EventLog usage to OpenTelemetry usage

Version control and branch strategy
- Create branch: `appmod/dotnet-migration-EventLog-to-OpenTelemetry-[CurrentTimestamp]` (timestamp format: yyyyMMddHHmmss)
- Stash uncommitted changes (including untracked files) prior to creating the branch, excluding `.appmod/` folder.
- Commit after each task with concise messages.

CVE check
- After adding packages, run `check_cve_vulnerability` on the added packages (only packages added during migration).

Build verification
- After code changes, run a full build to ensure compilation.

Notes and constraints
- No business logic changes beyond telemetry initialization and safe instrumentation.
- No production deployment steps will be executed in this migration.
- If the project is later hosted in ASP.NET or converted to Generic Host, the `TelemetryInitializer` can be adapted to use `IServiceCollection`/`IHostBuilder` patterns.

Next steps (require user confirmation)
1. Create `.appmod/.migration/progress.md` and commit plan (done now).
2. Prepare git (stash, create branch) and wait for your confirmation (`Continue`) to start executing migration tasks in order.

Reference: used `#open_telemetry_on_azure_knowledge_base` to determine packages and recommended initialization patterns.
