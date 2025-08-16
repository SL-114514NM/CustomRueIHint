# CustomRueIHint
# 这TM是什么
这个框架是基于RueI写的显示框架，能更简单地显示内容
# 安装方法
确保已经安装[RueI](https://github.com/pawslee/RueI)\
将"CustomRueIHint.dll"安装到插件依赖文件夹里即可(取决于你使用地是什么插件框架)
# 开发者开发使用方法
先创建一个CustomRueIHint.Hints.Hint
```
CustomRueIHint.Hints.Hint hint = new CustomRueIHint.Hints.Hint()
{
};
player.ReferenceHub.AddHint(hint);//添加提示
player.ReferenceHub.RemoveHint(hint);//移除提示
player.ReferenceHub.IsShow(hint.ID);//是否正在显示这个提示
```
Hint地属性:\
ShowTime - 显示时间默认5秒\
Text - 内容-静态\
ID - 这个hint的ID\
AutoText - 自动更新文字\
VerticalPosition - Y坐标\
VerticalPositionType - Y坐标预设\
[!]VerticalPositionType和VerticalPosition属性只能选择一个
# RueIHintManager
有几个方法可以通过ID获取指定的hint\
Get\
TryGet\
还有一个方法"HideAfter"\
参数为hint,功能为让指定的hint在指定的秒数后消失\
