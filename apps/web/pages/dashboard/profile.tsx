import { SkeletonText } from "@chakra-ui/react"
import { useQuery } from "@tanstack/react-query"
import type { NextPage } from "next"
import { useRouter } from "next/router"
import { api } from "../../api/apiClient"
import useMe from "../../api/hooks/useMe"
import { queryKeys } from "../../api/queryKeys"
import { DashboardContentWrapper } from "../../components/dashboard/DashboardContentWrapper/styles"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import BackgroundPhotoSection from "../../components/dashboard/profile/BackgroundPhotoSection"
import TopArtistsSection from "../../components/dashboard/TopArtistsSection"

export type UserProfile = {
  username?: string
  foregroundPhotoUri?: string | null
  backgroundPhotoUri?: string | null
  bio?: string | null
  city?: string | null
}

const UserProfile: NextPage = () => {
  const router = useRouter()
  const { profileName } = router.query

  const me = useMe()

  const { data, isLoading } = useQuery(
    [queryKeys.getUser, profileName],
    () =>
      api.identity.getUser({
        username: profileName as string,
      }),
    {
      enabled: !!profileName,
    }
  )

  const user = data?.value

  return (
    <DashboardLayout>
      <DashboardContentWrapper>
        <SkeletonText
          isLoaded={!!me?.username || !isLoading}
          noOfLines={5}
          skeletonHeight={10}
        >
          <BackgroundPhotoSection
            editable={!profileName}
            user={profileName ? user : me}
          />
        </SkeletonText>
        <TopArtistsSection />
      </DashboardContentWrapper>
    </DashboardLayout>
  )
}

export default UserProfile
