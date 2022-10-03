using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrpcServer.Utility {
    public class JWTService : IJWTService {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string GetToken(string userName) {
            //var userInfo = 
            Claim[] claims = new[]{
                new Claim(JwtRegisteredClaimNames.Name,userName),
                new Claim(JwtRegisteredClaimNames.Name,userName),
                new Claim(JwtRegisteredClaimNames.Sub,DateTime.Now.Ticks.ToString())
            };

            //获取配置文件安全秘钥信息
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            /**
            Claims (Payload)
            Claims部分包含了一些跟这个token 有关的重要信息。JWT 标准规定了一些字段，下面节选- -些字段:
            iss: The issuer of the token，token 是给谁的
            sub: The subject of the token, token 主题
            exp: Expiration Time。token 过期时间，Unix 时间戳格式
            iat: Issued At。token 创建时间，Unix 时间戳格式
            jti: JWT ID。针对当前token 的唯一标识
            除了规定的字段外，可以包含其他任何JSON 兼容的字段。
            * */
            var token = new JwtSecurityToken(
                //签发人
                issuer            : _configuration["issuer"],
                //与签发人要保持一致
                audience          : _configuration["audience"],
                claims            : claims,
                notBefore         : DateTime.Now,                    //token生效时间
                expires           : DateTime.Now.AddHours(24), //有效期
                signingCredentials: creds
            );
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }
    }
}
