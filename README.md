# Roslyn Analyzers

### Analyzers

#### Asynchronous analyzer

Support diagnostics: 
- Analyze if method has `Async` suffix
- Analyze if method has `CancellationToken` parameter
- Analyze if method's `CancellationToken` has name `cancellationToken`

Contains CodeFixes:
- Rename method name without `Async` suffix
- Add missing `CancellationToken cancellationToken` parameter
- Rename `CancellationToken` parameter to `cancellationToken` when name is not matched
