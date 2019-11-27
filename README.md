
# CoalMemory
**Simple library to interact with memory (read or write). Actually it's just a wrap on ReadProcessMemory and WriteProcessMemory.**

## Public members:
> **Get current process (property)**
> `public Process Process { get; set; }`

>  **Get .dll address of process. Returns library address.**
>  `public IntPtr GetLibraryAddress (string libraryName)`

> **Read double from memory. Returns double**
> `public double ReadDouble (IntPtr address)`

> **Read int from memory. Returns int**
> `public int ReadInt (IntPtr address)`

> **Read boolean from memory. Returns boolean**
> `public bool ReadBool (IntPtr address)`

> **Read string ASCII from memory. Returns string**
> `public string ReadStringASCII (IntPtr address)`

> **Read string UTF-8 from memory. Returns string**
> `public string ReadStringUTF8 (IntPtr address)`

> **Write value to memory (double/int/bool). Returns bool (true is successful)**
> `public bool WriteMemory (IntPtr address, double value)`
> `public bool WriteMemory (IntPtr address, int value)`
> `public bool WriteMemory (IntPtr address, bool value)`

> **Write string value to memory in different encodings. Returns bool (true is successful)**
> `public bool WriteStringASCII (IntPtr address, string value)`
> `public bool WriteMemoryUTF8 (IntPtr address, string value)`
