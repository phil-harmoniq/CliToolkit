#!/usr/bin/env bash

set -e
export CLR_RESET=$(tput sgr0)
export CLR_RED=$(tput setaf 1)

SCRIPT_PATH="$( cd "$(dirname "$0")" ; pwd -P )"
DLL_PATH="$SCRIPT_PATH/CliToolkit.Example/bin/Debug/netcoreapp2.1/CliToolkit.Example.dll"

if [[ ! -f "$DLL_PATH" ]]; then
    echo -e "$CLR_RED"'\nError: Unable to find the CliToolkit.Example binary'
    echo -e 'Please build the solution in Debug configuration before running this script\n'"$CLR_RESET"
    exit 1
fi

dotnet "$DLL_PATH" "$@"
