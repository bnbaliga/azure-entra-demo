#!/usr/bin/env bash
# PreToolUse hook: block Claude Code from editing config/production.json.
# Changes to that file must be made (or reviewed) by a human.
set -euo pipefail

input=$(cat)
file_path=$(jq -r '.tool_input.file_path // .tool_input.notebook_path // empty' <<<"$input")
[ -z "$file_path" ] && exit 0

project_dir="${CLAUDE_PROJECT_DIR:-$(jq -r '.cwd // empty' <<<"$input")}"
cwd=$(jq -r '.cwd // empty' <<<"$input")

# Resolve relative paths, "..", and symlinks so the check can't be sidestepped.
case "$file_path" in
  /*) ;;
  *) file_path="${cwd:-$project_dir}/$file_path" ;;
esac
target=$(realpath -m -- "$file_path")
protected=$(realpath -m -- "$project_dir/config/production.json")

if [ "$target" = "$protected" ]; then
  echo "Blocked: config/production.json must not be changed by Claude Code without human review. Describe the change you want and ask a human to make it." >&2
  exit 2
fi
exit 0
