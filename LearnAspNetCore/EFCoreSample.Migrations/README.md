## 项目说明

`Startup` 和 `DbContext` 类放在项目 `EFCoreSample` 下.  

`EFCoreSample.Migrations` 存放 `Migrations` 相关文件, 并引用 `EFCoreSample`.

1. 修改 `EFCoreSample.Migrations` 项目的生成输出路径  

修改为:
```xml
<PropertyGroup>
  <OutputPath>..\EFCoreSample\bin\$(Configuration)\</OutputPath>
</PropertyGroup>
```

2. 指定 `Migrations` 文件所在的程序集  

在 `EFCoreSample` 的 `DbContext` 中指定 `Migrations` 文件所在的程序集:
```csharp
 public class BlogDbContext : DbContext
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql("ConnectionString;", o =>
        {
            // 指定 Migrations 所在的程序集
            o.MigrationsAssembly("EFCoreSample.Migrations");     
        });

        base.OnConfiguring(optionsBuilder);
    }
}
```

3. 执行迁移命令

命令行下将路径切换到 `EFCoreSample`, 执行 `migrations` 命令:
```shell
dotnet ef database update --project ..\EFCoreSample.Migrations\
```

4. 新增迁移版本

如实体类新增属性, 那么添加一个迁移记录:
```
dotnet ef migrations add AddAuthorDescColumn --project ..\EFCoreSample.Migrations\
```


5. 重新编译 `EFCoreSample.Migrations`, 使最新程序集生成到 `EFCoreSample` 项目下.
6. 再执行步骤3, 数据库就更新成功.