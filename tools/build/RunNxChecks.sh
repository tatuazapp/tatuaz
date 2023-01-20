#!/usr/bin/env bash
nx format:write
nx run-many --target=build
nx run-many --target=test
nx run-many --target=lint
