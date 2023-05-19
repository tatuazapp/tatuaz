import { SkeletonText } from "@chakra-ui/react"
import { useQuery } from "@tanstack/react-query"
import type { NextPage } from "next"
import { useRouter } from "next/router"
import { api } from "../../api/apiClient"
import useMe from "../../api/hooks/useMe"
import { queryKeys } from "../../api/queryKeys"
import {
  DashboardContentWrapper,
  DashboardUserProfileSection,
} from "../../components/dashboard/DashboardContentWrapper/styles"
import DashboardLayout from "../../components/dashboard/DashboardLayout"
import BackgroundPhotoSection from "../../components/dashboard/profile/BackgroundPhotoSection"
import CreatePostSection from "../../components/dashboard/profile/CreatePostSection"
import UserPostsSection from "../../components/dashboard/profile/UserPostsSection"
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

  const currentUser = me?.username ?? user?.username ?? ""

  return (
    <DashboardLayout>
      <DashboardContentWrapper>
        <SkeletonText
          isLoaded={!!me?.username || !isLoading}
          noOfLines={5}
          skeletonHeight={10}
        >
          <DashboardUserProfileSection>
            <BackgroundPhotoSection
              editable={!profileName}
              user={profileName ? user : me}
            />

            <CreatePostSection />
            <UserPostsSection
              currentUser={currentUser}
              isEnabled={!!profileName || !!me?.username}
            />
          </DashboardUserProfileSection>
        </SkeletonText>
        <TopArtistsSection />
      </DashboardContentWrapper>
    </DashboardLayout>
  )
}

export default UserProfile
