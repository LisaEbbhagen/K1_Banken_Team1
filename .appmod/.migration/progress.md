# Migration progress: EventLog -> OpenTelemetry on Azure

Important Guideline:
1. When you use terminal command tool, never input a long command with multiple lines.
2. When performing semantic or intent-based searches, DO NOT search content from `.appmod/` folder.
3. Never create a new project in the solution, always use the existing project to add new files or update the existing files.
4. Minimize code changes: update only what's necessary for the migration.
5. Add new package references following SDK-style or legacy project guidance.
6. Task tracking: update tasks to - [in_progress] and - [X] as you work.
7. Version control integration: create branch, stash, commit, do not commit `.appmod/` files.

Migration checklist
- [ ] Check git status and stash uncommitted changes (if git present)
- [ ] Create branch: appmod/dotnet-migration-EventLog-to-OpenTelemetry-[CurrentTimestamp]
- [ ] Add OpenTelemetry packages to project
- [ ] Add `Presentation/Telemetry/TelemetryInitializer.cs`
- [ ] Edit `Program.cs` to call telemetry initializer
- [ ] Update any EventLog usages (none found) or provide conversion examples
- [ ] Run build and fix compilation errors
- [ ] Run CVE check for added packages
- [ ] Commit all changes per task
- [ ] Final build verification

Notes:
- Stop after creating these files and wait for user to confirm by typing `Continue` before making code changes.
