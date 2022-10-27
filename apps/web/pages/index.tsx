import { useAuth0 } from "@auth0/auth0-react"
import { SliceZone } from "@prismicio/react"
import {
  CenteredLayout,
  Profile,
  SignInButton,
  SignOutButton,
} from "@tatuaz/ui"
import { createClient } from "../prismicio"
import { components } from "../slices"

const Index = ({ page }) => {
  const { isAuthenticated } = useAuth0()

  return (
    <div>
      <CenteredLayout>
        {isAuthenticated ? (
          <>
            <Profile />
            <SignOutButton />
          </>
        ) : (
          <SignInButton />
        )}
      </CenteredLayout>
      <div>
        <SliceZone components={components} slices={page.data.slices} />
      </div>
    </div>
  )
}

export default Index

export async function getStaticProps({ previewData }) {
  const client = createClient({ previewData })

  const page = await client.getSingle("test")

  return {
    props: {
      page,
    },
  }
}
