export interface User {
  userName:	string,
  jwtToken:	string,
  refreshToken:	string,
  refreshTokenExpires: string,
  email: string,
  roles: string[]
}
