const { exec } = require('child_process');

if (process.env.SKIP_RESTORE) {
  process.exit(0);
}

exec('nx g @nx-dotnet/core:restore');
