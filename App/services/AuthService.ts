type TokenResponse = {
  access_token: string;
  refresh_token: string;
}

async function login(login: string, password: string, clientId: string, clientSecret: string): Promise<TokenResponse | null> {
  var response = await fetch('https://localhost:6932/api/auth/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    },
    body: new URLSearchParams({
      'username': login,
      'password': password,
      'grant_type': 'password',
      'scope': 'offline_access',
      'client_id': clientId,
      'clientSecret': clientSecret
    }).toString()
  });

  if (response.status == 401) {
    return null;
  }

  return response.json();
}

async function refresh(refreshToken: string, clientId: string, clientSecret: string): Promise<TokenResponse | null> {
  var response = await fetch('https://localhost:6932/api/auth/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    },
    body: new URLSearchParams({
      'refresh_token': refreshToken,
      'grant_type': 'refresh_token',
      'scope': 'offline_access',
      'client_id': clientId,
      'clientSecret': clientSecret
    }).toString()
  });

  if (response.status != 200) {
    return null;
  }

  return response.json();
}

export default {
  login,
  refresh
}
