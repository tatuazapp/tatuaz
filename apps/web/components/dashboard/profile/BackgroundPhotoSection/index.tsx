import { FunctionComponent, useState } from "react"
import useMe from "../../../../api/hooks/useMe"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import { BackgroundPhotoContainer } from "../BackgroundPhotoContainer/styles"
import BackgroundPhotoUploadModal from "../BackgroundPhotoUploadModal"

const BackgroundPhotoSection: FunctionComponent = () => {
  const [isOpen, setIsOpen] = useState(false)

  const handleBackgroundPhotoClick = () => {
    setIsOpen(true)
  }

  const me = useMe()

  return (
    <>
      <BackgroundPhotoContainer
        imageUrl={
          me?.backgroundPhotoUri && formatCDNImageUrl(me?.backgroundPhotoUri)
        }
        onClick={handleBackgroundPhotoClick}
      />
      <BackgroundPhotoUploadModal
        isOpen={isOpen}
        onClose={() => setIsOpen(false)}
      />
    </>
  )
}

export default BackgroundPhotoSection
