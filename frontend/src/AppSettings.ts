export const server = 'https://localhost:7124';

export const webAPIUrl = `${server}/api`;

export const authSettings = {
  domain: 'dev-vj214tpv.us.auth0.com',
  client_id: 'xv0s0btz6CCfvDHp6GYpeZVPsJFCHLIn',
  redirect_uri: window.location.origin + '/signin-callback',
  scope: 'openid profile QandAAPI email',
  audience: 'https://qanda',
};
