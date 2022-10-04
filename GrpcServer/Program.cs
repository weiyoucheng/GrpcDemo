using GrpcServer.Services;
using GrpcServer.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
//ע��JWT����
services.AddScoped<IJWTService, JWTService>();
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
//ע��Grpc����
services.AddGrpc(option =>
{
    //��ȡ������һ��ֵ����ֵָʾgRPC�Ƿ�Ӧ���Զ�δ֪����ͷ����ĵ��á�
    //�������Ϊtrue����δ֪����ͷ����ĵ��ý����᷵�ء�UNIMPLEMENTED��
    //״̬�����󽫴��ݸ�ASP�е���һ��ע���м����NET���ġ�
    //option.IgnoreUnknownServices = true;
    //��Ӧ�ļ�ѹ������
    option.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
    //����������������ݴ�С�����ֽ�Ϊ��λ��
    option.MaxSendMessageSize = (4 * 1024 * 1024) * 10;
    //�����������������ݴ�С�����ֽ�Ϊ��λ��
    option.MaxReceiveMessageSize = (4 * 1024 * 1024) * 10;
});
//ע�����ʶ��ʹ���������ȷ�����ṩ����
services.AddCodeFirstGrpc();

//���Grpc�����������PostMan�Ϳ���ֱ�ӷ����ȡ�ӿ���Ϣ�����ô���Proto�ļ�
//��Nuget��� protobuf-net.Grpc.AspNetCore.Reflection ����
services.AddCodeFirstGrpcReflection();

//��ȡ�����ļ�
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var validAudience = configuration.GetValue<string>(Constants.VALIDAUDIENCE);
var validIssuer   = configuration.GetValue<string>(Constants.VALIDISSUER);
//��ȫ��Կ
var securityKey   = configuration.GetValue<string>(Constants.SECURITYKEY);
//ע����Ȩ��֤����
services.AddAuthorization();
//ע�������֤��������ķ����Լ����Jwt��֤
services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        //��ȡ������������֤��ʶ���ƵĲ�����
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,          //�Ƿ���֤�䷢��  
            ValidateAudience         = true,          //�Ƿ���֤Audience
            ValidateLifetime         = true,          //�Ƿ���֤ʧЧʱ��
            ValidateIssuerSigningKey = true,          //�Ƿ���֤SecurityKey
            ValidAudience            = validAudience, //Audience
            ValidIssuer              = validIssuer,   //Issuer.�������ǰ��ǩ��jwt������- -�±�ʾ˭ǩ����Token
            //ע�����ǻ������ʱ�䣬�ܵ���Чʱ��������ʱ�����jwt�Ĺ���ʱ�䣬��������ã�Ĭ����5����
            ClockSkew                = TimeSpan.FromSeconds(60 * 5),
            IssuerSigningKey         = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityKey))//�õ�SecurityKey
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.MapControllers();

//ʹ��·�ɲ�ȻPostMan���䲻�ɹ�
app.UseRouting();

//ʹ����Ȩ��֤����
app.UseAuthorization();
//ʹ�������֤���������д�� Authorization �������ᱨ��
app.UseAuthentication();

app.UseEndpoints(endpoints => {
    //���Grpc����
    endpoints.MapGrpcService<LoginService>();
    endpoints.MapGrpcService<WaybillInfoService>();
    //����Grpc���������Nuget���� protobuf-net.Grpc.AspNetCore.Reflection
    endpoints.MapCodeFirstGrpcReflectionService();
});

app.Run();
