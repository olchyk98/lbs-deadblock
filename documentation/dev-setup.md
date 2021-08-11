# Dev Setup
Main information about the setup for this project can be found in the "Initial Planning" section.
This part explains the chosen tooling on a low level, combined with motivation and reasons for
different solutions in the project.

## Github + CircleCI
This is a popular combination in most of my projects since it's really easy
to launch your workflow on ***`CircleCI`***. The direct integration with Github
provided by ***`CircleCI`*** saved a lot of time that was eventually spent on development.

The CI tool works using push events. It is defined in the configuration file, to listen
to all the existing branches. That was done, to make PRs more verbose. It also
added an extra layer of protection when merging new changes.

## Rebasing, instead of merging
I find rebasing much more satisfying when it comes to performance and results.
It's always possible to merge, but that can lead to unexpected errors and extra commits messages,
that potentially should be then squashed, or changed somehow. Merging for this project was
disabled, therefore there are no extra commits on the master branch that indicate any
forced merges. This keeps the branch clean, and therefore it's really easy to just go
through all changes. Without the need of searching for the real commits.

## Dotnet.ObjectDumper for debugging
Since I use ***`NeoVim`*** for C# development (and I don't like ***`vimspector`***), I decided to use
an external package for debugging. It's a great tool for quick instance debugging. Of course, it's not
a production-level tool. That would be much better to have a logging pipeline instead, but that would take
time to build something like that. Since this is not a production project, it doesn't need a thing like that.

## Dotnet SDK (3 & 5)
I had to use a different version of Dotnet, to make my building/testing pipeline faster, since
some of the testing tools presented in this project work better on a specific version of Dotnet.
You don't need to have multiple versions of Dotnet to build the project.
Dotnet LTS version is only used in the cloud solution.
