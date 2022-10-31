import { useAuth0 } from "@auth0/auth0-react"
import { Center } from "@chakra-ui/react"
import AppLayout from "../components/auth/AppLayout"
import { Profile } from "../components/auth/Profile"

const Index = () => {
  const { isAuthenticated, isLoading } = useAuth0()

  return (
    <AppLayout>
      <Center h="400px">
        {isAuthenticated || isLoading ? <Profile /> : null}
      </Center>
    </AppLayout>
  )
}

export default Index
