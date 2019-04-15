# Diagnostic ID: AANA002

| Property               | Value                                                             |
| ---------------------- | ------------------------------------------------------------------|
| Id                     | AANA002                                                           |
| Category				 | `Naming`										                     |
| Title                  | `CancellationToken` name is different than `cancellationToken` |
| Severity               | `Error`                                                           |
| Official Documentation |                                                                    |

<br />

_Method signature:_

```
public async void MyTaskMethod(CancellationToken ct)

public Task MyTaskMethod(CancellationToken ct)
public Task<T> MyTaskTMethod(CancellationToken ct)

public async Task MyTaskMethod(CancellationToken ct)
public async Task<T> MyTaskTMethod(CancellationToken ct)
```

_Expected method signature:_

```
public async void MyTaskMethodAsync(CancellationToken cancellatiuonToken)

public Task MyTaskMethodAsync(CancellationToken cancellatiuonToken)
public Task<T> MyTaskTMethodAsync(CancellationToken cancellatiuonToken)

public async Task MyTaskMethodAsync(CancellationToken cancellatiuonToken)
public async Task<T> MyTaskTMethodAsync(CancellationToken cancellatiuonToken)
```

<br/>

#### Severity

__Error:__

 - `async void` methods  
 - `Task` methods
 - `Task<T>` methods
 - `async Task` methods
 - `async Task<T>` methods

#### Code Fixes
Rename `CancellationToken` to name `cancellationToken`.