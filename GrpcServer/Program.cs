using GrpcServer.Services;
using GrpcServer.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
//注册JWT服务
services.AddScoped<IJWTService, JWTService>();
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
//注册Grpc服务
services.AddGrpc(option =>
{
    //获取或设置一个值，该值指示gRPC是否应忽略对未知服务和方法的调用。
    //如果设置为true，对未知服务和方法的调用将不会返回“UNIMPLEMENTED”
    //状态，请求将传递给ASP中的下一个注册中间件。NET核心。
    //option.IgnoreUnknownServices = true;
    //响应文件压缩级别
    option.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
    //设置允许最大发送数据大小（以字节为单位）
    option.MaxSendMessageSize = (4 * 1024 * 1024) * 10;
    //设置允许最大接收数据大小（以字节为单位）
    option.MaxReceiveMessageSize = (4 * 1024 * 1024) * 10;
});
//注册可以识别和处理代码优先服务的提供程序
services.AddCodeFirstGrpc();

//添加Grpc反射服务，这样PostMan就可以直接反射获取接口信息而不用创建Proto文件
//需Nuget添加 protobuf-net.Grpc.AspNetCore.Reflection 引用
services.AddCodeFirstGrpcReflection();

//读取配置文件
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var validAudience = configuration.GetValue<string>(Constants.VALIDAUDIENCE);
var validIssuer   = configuration.GetValue<string>(Constants.VALIDISSUER);
//安全秘钥
var securityKey   = configuration.GetValue<string>(Constants.SECURITYKEY);
//注册授权验证策略
services.AddAuthorization();
//注册身份验证服务所需的服务。以及添加Jwt验证
services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        //获取或设置用于验证标识令牌的参数。
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,          //是否验证颁发这  
            ValidateAudience         = true,          //是否验证Audience
            ValidateLifetime         = true,          //是否验证失效时间
            ValidateIssuerSigningKey = true,          //是否验证SecurityKey
            ValidAudience            = validAudience, //Audience
            ValidIssuer              = validIssuer,   //Issuer.这两项和前面签发jwt的设置- -致表示谁签发的Token
            //注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
            ClockSkew                = TimeSpan.FromSeconds(60 * 5),
            IssuerSigningKey         = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityKey))//拿到SecurityKey
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.MapControllers();

//使用路由不然PostMan反射不成功
app.UseRouting();

//使用授权验证策略
app.UseAuthorization();
//使用身份验证（这个必须写在 Authorization 后面否则会报错）
app.UseAuthentication();

app.UseEndpoints(endpoints => {
    //添加Grpc服务
    endpoints.MapGrpcService<LoginService>();
    endpoints.MapGrpcService<WaybillInfoService>();
    //启用Grpc反射服务，需Nuget引用 protobuf-net.Grpc.AspNetCore.Reflection
    endpoints.MapCodeFirstGrpcReflectionService();
});

app.Run();
