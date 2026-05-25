import { useLogin } from "../hooks/useLogin";

const LoginForm = () => {
  const {
    email,
    setEmail,
    password,
    setPassword,
    rememberMe,
    setRememberMe,
    handleLogin,
    isPending,
    isSuccess,
    isError,
    errorMessage,
  } = useLogin();

  return (
    <form onSubmit={handleLogin}>
      <input
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        placeholder="Email"
        disabled={isPending || isSuccess}
      />
      <input
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Password"
        disabled={isPending || isSuccess}
      />
      <input
        type="checkbox"
        checked={rememberMe}
        onChange={(e) => setRememberMe(e.target.checked)}
        disabled={isPending || isSuccess}
      />
      <button type="submit" disabled={isPending || isSuccess}>
        {isPending ? "Signing in…" : isSuccess ? "Redirecting…" : "Sign in"}
      </button>

      {isSuccess && <p>Login successful!</p>}
      {isError && <p>{errorMessage}</p>}
    </form>
  );
};

export default LoginForm;
