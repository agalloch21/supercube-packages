# supercube-package
Effect packages for project SuperCube 

---
## Requirements
- Unity 6000.0.5
- HDRP
- Visual Effect Graph

## Dependancies
- Cinemachine 3
- xiaobo.unity.toolkit from [Xiaobo](https://github.com/agalloch21/xiaobo.unity.toolkit.git)

## Structure
<div>
<img src="Documentation~/folder.png"/>
</div><br>

- ```xiaobo.supercube.interaction``` Scene that's responsible for change input to texture. Can drag this scene into project if want to add interaction.
- ```xiaobo.supercube.lava``` Scene that includes Lava effect. Can drag this scene into project or copy gameobjects out.
- ```xiaobo.supercube.environment``` Basic environment setup. No need to use this one.
- ```xiaobo.supercube.common``` Utilities. No need to use this one.

## Extra Settings
- Add Layer "OnsiteSetup" at index 7
- Add Layer "InteractionLayer" at index 8
- Add Layer "LavaParticle" at index 11
- Add Layer "Lava" at index 12
 <div>
<img src="Documentation~/layer.png"/>
</div><br>
