param(
    $ParallelThreads = 3
)

nx format:write
nx run-many --target=build --parallel=$ParallelThreads --skip-nx-cache --nx-bail --output-style=stream
nx run-many --target=test --parallel=$ParallelThreads --skip-nx-cache --nx-bail --output-style=stream
nx run-many --target=lint --parallel=$ParallelThreads --skip-nx-cache --nx-bail --output-style=stream
