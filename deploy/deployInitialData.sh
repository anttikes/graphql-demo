#! /usr/bin/bash
curl localhost:32741 \
  -F operations='{ "query": "mutation ($file: Upload!) { uploadInitialData(file: $file) { id }}", "variables": { "file": null } }' \
  -F map='{ "0": ["variables.file"] }' \
  -F 0=@deploy/movies-compact.json
