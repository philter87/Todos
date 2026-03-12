# Create copilot-instructions

Options
- /init 
- VS Code: Tandhjul --> 'Instructions & Rules' -> 'Generate agent instructions'

### Instruction types
You can defined instructions for different types of instructions.

- **Repository-wide**: Example: .github/copilot-instructions.md
- **Path-specific instructions**: Example: .github/instructions/testing.instructions.md
- **Agent instructions (preview)**: Example: Todos/src/AGENTS.md file. Stored anywhere in the repository, the cloest takes precendence


```bash
git checkout feat/instructions-1
```