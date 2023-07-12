<h3><p align="center"><b>No Battle Knight</b></p></h3>

---

**工程版本号**

> Unity 2021.3.10f1c1



**项目文件目录**

> * Asset
>   * Animation
>   * Prefabs
>   * Resource
>   * Scenes
>   * Scripts
>   * Settings
>   * StreamingAssets
>   * TextMesh Pro









----

> date：2023.5.28



**some tips:**

* 画布的尺寸固定为1600*900



##### 0_main

* 过渡动画/异步中间动画

  > 这种方法每个场景都要在画布的最前面加一块全黑的`rawimage`，目前没有找到适合的禁用方法，所以这就要求每个场景在运行前`rawimage`都是激活的，这在编辑时不方便，建议每次运行时再激活
  >
  > `一般是画布下的最后一个`建议编辑前将所有场景的RawImage全都禁用，运行时再打开
  >
  > 使用的是渐入渐出[Unity场景切换，屏幕过场淡入淡出_unity 转场_真像大白阿的博客-CSDN博客](https://blog.csdn.net/mango9126/article/details/79759750)
  >
  > 脚本挂载到RawImage下，每次运行先将RawImage设置为Active（编辑时关闭是因为影响视觉效果）
  >
  > 在加载场景前会有过渡效果
  >
  > **后面可能要优化一下**

* 按钮的点击事件

  按钮的点击事件，一般都在每一个场景里加一个空物体，然后把脚本放到空物体上，在用鼠标的点击事件调用对应的函数

  **几乎所有场景跳转的代码统一放在一个脚本里了**



##### 1_choose

* 按钮的排列

  按钮的排列是通过`Grid Layout Group`实现的，每一个按钮下面挂了所有的星星和锁，根据不同的得分选择禁用哪些图片，默认全部禁用，得分的星星数用`static`数组存在这个脚本里，其他脚本使用时可以直接通过`脚本名.变量`的方法获取



##### 2_level1

* 人物

  Player下面的character才是真的图像，（宏观）Player是用来挂载脚本和碰撞体刚体等的空物体，（微观）动画挂载在character上，获取对象组件的时候不要搞错了

* 动画

  动画功能基本实现了，其中jump的动画只有一个触发器，其他是两个，后续可以看情况再修改，触发器目前有着5种

  ![image-20230518195909362](https://cdn.jsdelivr.net/gh/cyqcw/ImageStore@main/202305181959564.png)





* 跳跃

  实现了只在地面上的时候跳跃。（利用Layer来检测地面）
  
* 攀爬

  * 在梯子上的时候能会触发攀爬模式，此时按w可以向上移动
  * 梯子的预制体Ladder在prefabs文件夹中，直接使用即可
  * ![image-20230528234400477](C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\image-20230528234400477.png)

* 场景的遮挡与现实

  * 做好了预制体blackScreen，直接使用即可![image-20230528234601706](C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\image-20230528234601706.png)

* 星星的收集与统计

  * 做好了预制体star，上面的脚本实现了starCollection统计的功能
  * 统计的数据存放在Assets\StreamingAssets的txt文件中，每一个关卡对应一个文件![image-20230528235041368](C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\image-20230528235041368.png)
  * 每次进入关卡时会把对应文件中的数字清零
  * 当star被收集时，对应文件的数字会+1
  * Check_Level_Controller会读取txt文件来在选关场景中渲染星星

* 回到选关界面

  * 目前的话主角走到门下按下w即可进入选关页面，后面还要实现先用钥匙开门，才能过关
  * 选关界面会现实出上一次游戏中的星星数（不是最高记录）

  

