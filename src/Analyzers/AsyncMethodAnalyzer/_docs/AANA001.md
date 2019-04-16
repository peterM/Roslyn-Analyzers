# Diagnostic ID: AANA001

| Property               | Value                                           |
| ---------------------- | ------------------------------------------------|
| Id                     | AANA001                                         |
| Category				 | `Naming`										   |
| Title                  | Method's name missing `Async` suffix           |
| Severity               | `Info`                                         |
| Official Documentation |                                                 |

<br />

_Method signature:_

```
public async void MyTaskMethod()

public Task MyTaskMethod()
public Task<T> MyTaskTMethod()

public async Task MyTaskMethod()
public async Task<T> MyTaskTMethod()
```

_Expected method signature:_

```
public async void MyTaskMethodAsync()

public Task MyTaskMethodAsync()
public Task<T> MyTaskTMethodAsync()

public async Task MyTaskMethodAsync()
public async Task<T> MyTaskTMethodAsync()
```

<br/>

#### Severity

__Info:__

 - `async void` methods
 - `Task` methods
 - `Task<T>` methods
 - `async Task` methods
 - `async Task<T>` methods

#### Code Fixes
Rename method and add `Async` suffix to name.