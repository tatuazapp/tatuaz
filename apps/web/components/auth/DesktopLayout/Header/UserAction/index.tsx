import { useAuth0 } from "@auth0/auth0-react"
import { FunctionComponent } from "react"
import { SignInButton } from "../../../SignInButton"
import AvatarMenu from "./AvatarMenu"

type UserActionProps = {
  children?: React.ReactNode
}

const UserAction: FunctionComponent<UserActionProps> = () => {
  const { isAuthenticated, isLoading, user } = useAuth0()
  if (isLoading) return null

  if (isAuthenticated) return <AvatarMenu user={user} />

  return <SignInButton />
}

export default UserAction
