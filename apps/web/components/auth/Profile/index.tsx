import { useAuth0 } from "@auth0/auth0-react"
import { Spinner } from "@chakra-ui/react"
import Image from "next/image"
import useMe from "../../../api/hooks/useMe"
import { ProfileWrapper } from "./styles"

export const Profile = () => {
  const { user, isAuthenticated, isLoading } = useAuth0()

  console.log("user", user)

  const user2 = useMe()

  console.log("data", user2)

  if (isLoading) return <Spinner color="red.500" />

  return isAuthenticated ? (
    <ProfileWrapper>
      <Image
        alt={user.name}
        height={80}
        referrerPolicy="no-referrer"
        src={user.picture}
        width={80}
      />
      <h2>{user.name}</h2>
      <p>{user.email}</p>
    </ProfileWrapper>
  ) : null
}
