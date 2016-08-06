# SharedMediaManager
### Build Status
[![Build Status](https://travis-ci.org/SharedMediaManagerGroup/SharedMediaManager.svg?branch=master)](https://travis-ci.org/SharedMediaManagerGroup/SharedMediaManager) master 

[![Build Status](https://travis-ci.org/SharedMediaManagerGroup/SharedMediaManager.svg?branch=dev)](https://travis-ci.org/SharedMediaManagerGroup/SharedMediaManager) dev
### Branch Management
#### Naming
- dev: current version branch
- userstory/xyz implementation of userstory xyz 
- bugfix/xyz bugfix xyz
- experimental/xyz experiments that could be interestin for all developers

#### dev
This is the current version branch on which the current milestone is developed. The pull request to the master branch needs a referencze
to the milestone that is currently worked on.

#### userstory
Implementation of one single userstory. Pull request to dev branch needs a referent to the issue describing the userstory.

#### bugfix
Implementation of one single bugfix. Pull request to the brach for the bugfix needs a referenc to an issue describing the problem.

#### Merging
Pull request can only be merged if all acceptency criterias are met, the continouse integration server has no problems with the pull and the pull is approved by at least one other developer.

### Code Conventions
We use the normal [msdn](https://msdn.microsoft.com/en-us/library/ff926074.aspx) expect for using K&R braces style.
```
if (x == y) {
    x++;
    foo();
} else {
    x--;
    bar();
}
```

Also we use a tab size of 2 spaces. All of this can be configured in Visual Studio.

### Versioning
We use the standart [.net](https://msdn.microsoft.com/en-us/library/system.version(v=vs.110).aspx) versioning system. 

