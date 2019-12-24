export const environment = {
  production: true,
  apiUrl: 'https://api.casn.app/api',
  clientRoot: 'https://casn.app/',
  customConfigUrl: 'https://casn.app/customconfig.json',
  get logoutRedirectUrl() { return `${this.clientRoot}assets/oidc-login-redirect.html`; },
};
