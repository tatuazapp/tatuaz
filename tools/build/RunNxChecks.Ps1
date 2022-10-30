param(
    $ParallelThreads = 3
)

nx format:write
dotnet format
nx run-many --target=build --parallel=$ParallelThreads --skip-nx-cache
nx run-many --target=test --parallel=$ParallelThreads --skip-nx-cache
nx run-many --target=lint --parallel=$ParallelThreads --skip-nx-cache
