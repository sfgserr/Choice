export type Auth = {
  signIn: (accessToken: string, refreshToken: string) => void;
  signOut: () => void;
  restore: () => void;
}
