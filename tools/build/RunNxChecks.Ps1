nx format:write
dotnet format
nx run-many --target=build
nx run-many --target=test
nx run-many --target=lint
