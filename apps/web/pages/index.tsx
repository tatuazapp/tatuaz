import { useAuth0 } from "@auth0/auth0-react"
import { Container } from "@chakra-ui/react"
import AppLayout from "../components/auth/AppLayout"
import { Profile } from "../components/auth/Profile"
import { SignInButton } from "../components/auth/SignInButton"

const Index = () => {
  const { isAuthenticated, isLoading } = useAuth0()

  return (
    <AppLayout>
      <Container centerContent>
        {isAuthenticated || isLoading ? <Profile /> : <SignInButton />}
      </Container>
    </AppLayout>
  )
}

export default Index
