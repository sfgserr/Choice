
type Response = {
  access_token: string;
  refresh_token: string;
}

async function login(login: string, password: string): Promise<Response | null> {
  var response = await fetch('https://localhost:6932/api/auth/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    },
    body: new URLSearchParams({
      'username': login,
      'password': password,
      'grant_type': 'password',
      'scope': 'offline_access'
    }).toString()
  });

  if (response.status == 401) {
    return null;
  }

  return response.json();
}

export default {
  login
}
