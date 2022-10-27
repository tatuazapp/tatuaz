import { useAuth0 } from "@auth0/auth0-react"
import { ProfileWrapper } from "./styles"

export const Profile = () => {
  const { user, isAuthenticated, isLoading } = useAuth0()

  if (isLoading)
    return <>Loading ... tipsy większe niż parówy, gdy się widzi z kolegami</>

  if (!user) return <>Brak obiektu user. XS stanik, weź bitch, weź zamilcz</>

  return isAuthenticated ? (
    <ProfileWrapper>
      <img alt={user.name} src={user.picture} />
      <h2>{user.name}</h2>
      <p>{user.email}</p>
    </ProfileWrapper>
  ) : null
}
