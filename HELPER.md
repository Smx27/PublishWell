### Few cool tricks that I learn during this project 
Find and replace is awsome and can be found in any text editor my usecase is i want to change my namespace with a specific format 
EX- `sample.someapp.foo` I want to make it like `sample.api.someapp.foo` the cool regex is to find the groups
```regex
sample\.([^.]+)
```
and to replace this 
```regex
sample.API.$1
```
This is the command to set a specific node version as default using nvm 

```bash
nvm alias default 20.12.2
```