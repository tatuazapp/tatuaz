import { useAuth0 } from "@auth0/auth0-react"
import { ButtonWrapper } from "./styles"

const LoginItem = () => {
  const { isAuthenticated, isLoading } = useAuth0()
  const { loginWithRedirect, logout } = useAuth0()
  if (isLoading) return null

  const onClickHandler = () => {
    isAuthenticated
      ? logout({ returnTo: process.env["NEXT_PUBLIC_AUTH0_REDIRECT_URI"] })
      : loginWithRedirect()
  }

  const title = isAuthenticated ? "Wyloguj" : "Zaloguj"

  return <ButtonWrapper onClick={onClickHandler}>{title}</ButtonWrapper>
}

export default LoginItem
