param(
    $ParallelThreads = 3,
    [Boolean]
    $SkipNxCache = 1
)

nx format:write
dotnet format
nx run-many --target=build --parallel=$ParallelThreads --skip-nx-cache=$SkipNxCache
nx run-many --target=test --parallel=$ParallelThreads --skip-nx-cache=$SkipNxCache
nx run-many --target=lint --parallel=$ParallelThreads --skip-nx-cache=$SkipNxCache
