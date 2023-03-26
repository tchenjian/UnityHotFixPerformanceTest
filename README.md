# UnityHotFixPerformanceTest
Unity热更方案性能测试,包含HybridCLR,Xlua,InjectFix以及C#原生的性能测试工程

# 测试环境:
雷电模拟器

HybridClr, Unity版本 2020.3.33f1c2
Native,xlua,InjectFix Unity版本 2020.3.46f1c1

打包配置
![image](https://user-images.githubusercontent.com/18399445/227775596-d62ad5a7-65f1-4694-b0cd-50b3797fef3d.png)
其中 InjectFix IL2CPP异常, 打的Mono包

# 测试结果:
![Native](https://user-images.githubusercontent.com/18399445/227775371-491cd4db-b39f-44b9-ab49-1c6df3e9900b.png)
![HybridClr](https://user-images.githubusercontent.com/18399445/227775363-c72fc867-6255-4faa-a571-632d81f32004.png)
![lua](https://user-images.githubusercontent.com/18399445/227775376-eb19e8a1-8e0b-4132-afd8-4a4448c68280.png)
![InjectFix](https://user-images.githubusercontent.com/18399445/227775366-e1471685-9e12-44ef-b154-426fb9973197.png)
