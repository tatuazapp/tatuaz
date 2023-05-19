import { Heading4 } from "@tatuaz/ui"
import { FunctionComponent, useState } from "react"
import ClampLines from "react-clamp-lines"
import { useIntl } from "react-intl"
import { UserProfile } from "../../../../pages/dashboard/profile"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import AvatarUploadModal from "../AvatarUploadModal"
import {
  AvatarContainer,
  BackgroundAndAvatarContainer,
  BackgroundPhotoContainer,
  BioContainer,
  UserInfoContainer,
} from "../BackgroundAndAvatarContainer/styles"
import BackgroundPhotoUploadModal from "../BackgroundPhotoUploadModal"
import EditBioModal from "../EditBioModal"

const MAX_BACKGROUND_PHOTO_SIZE = 1024
const MAX_AVATAR_SIZE = 512

type BackgroundPhotoSectionProps = {
  user: UserProfile | undefined | null
  editable?: boolean
}

const BackgroundPhotoSection: FunctionComponent<
  BackgroundPhotoSectionProps
> = ({ user, editable }) => {
  const [isBackgroundModalOpen, setIsBackgroundModalOpen] = useState(false)
  const [isAvatarModalOpen, setIsAvatarModalOpen] = useState(false)
  const [isEditBioModalOpen, setIsEditBioModalOpen] = useState(false)

  const intl = useIntl()

  const handleBackgroundPhotoClick = () => {
    setIsBackgroundModalOpen(true)
  }

  const handleAvatarPhotoClick = () => {
    setIsAvatarModalOpen(true)
  }

  const handleEditBioClick = () => {
    setIsEditBioModalOpen(true)
  }

  return (
    <>
      <BackgroundAndAvatarContainer>
        <BackgroundPhotoContainer
          imageUrl={
            user?.backgroundPhotoUri &&
            formatCDNImageUrl(user?.backgroundPhotoUri, {
              width: MAX_BACKGROUND_PHOTO_SIZE,
            })
          }
          isEditable={editable}
          onClick={editable ? handleBackgroundPhotoClick : undefined}
        />
        <AvatarContainer
          isEditable={editable}
          src={formatCDNImageUrl(user?.foregroundPhotoUri ?? "", {
            maxWidth: MAX_AVATAR_SIZE,
          })}
          onClick={editable && handleAvatarPhotoClick}
        />
        <UserInfoContainer>
          <Heading4>{user?.username ?? "-"}</Heading4>
          <BioContainer
            isEditable={editable}
            isEmpty={!user?.bio}
            onClick={editable ? handleEditBioClick : undefined}
          >
            {!!user && (
              <ClampLines
                id={user?.bio ?? "user-bio" + user?.username}
                lessText={intl.formatMessage({
                  defaultMessage: "Mniej",
                  id: "feQNnn",
                })}
                lines={2}
                moreText={intl.formatMessage({
                  defaultMessage: "Pokaż więcej",
                  id: "UC6hZD",
                })}
                stopPropagation={true}
                text={
                  user?.bio
                    ? user.bio
                    : editable
                    ? intl.formatMessage({
                        defaultMessage: "Kliknij, aby dodać opis",
                        id: "7Fsz7M",
                      })
                    : ""
                }
              />
            )}
          </BioContainer>
        </UserInfoContainer>
      </BackgroundAndAvatarContainer>

      <BackgroundPhotoUploadModal
        isOpen={isBackgroundModalOpen}
        onClose={() => setIsBackgroundModalOpen(false)}
      />
      <AvatarUploadModal
        isOpen={isAvatarModalOpen}
        onClose={() => setIsAvatarModalOpen(false)}
      />
      <EditBioModal
        initialValues={{
          bio: user?.bio ?? undefined,
          city: user?.city ?? undefined,
        }}
        isOpen={isEditBioModalOpen}
        onClose={() => setIsEditBioModalOpen(false)}
      />
    </>
  )
}

export default BackgroundPhotoSection
