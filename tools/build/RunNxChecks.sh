#!/usr/bin/env bash

THREADS=${1:-3}

echo "Using $THREADS threads"

nx format:write
nx run-many --target=build --parallel=$THREADS --skip-nx-cache --nx-bail --output-style=stream
nx run-many --target=test --parallel=$THREADS --skip-nx-cache --nx-bail --output-style=stream
nx run-many --target=lint --parallel=$THREADS --skip-nx-cache --nx-bail --output-style=stream
