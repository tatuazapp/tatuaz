import {
  Modal,
  ModalOverlay,
  ModalContent,
  useDisclosure,
  Drawer,
  DrawerOverlay,
  DrawerContent,
} from "@chakra-ui/react"
import { Paragraph, Heading } from "@tatuaz/ui"
import { useState } from "react"
import { theme } from "../../../../styles/theme"
import useIsTablet from "../../../../utils/hooks/useIsTablet"
import ArtistPostCommentView from "./ArtistPostCommentView"
import {
  ArtistPostContent,
  ArtistPostDescription,
  ArtistPostLikesAndCommentsWrapper,
  ArtistPostMainPhoto,
  ArtistPostMainPhotoTitle,
  ArtistPostUserWrapper,
  ArtistPostWrapper,
  CommentsContainer,
  CommentSectionClickedIcon,
  CommentSectionNotClickedIcon,
  CommentsNumber,
  LikedPhotoIcon,
  LikesContainer,
  LikesNumber,
  NotLikedPhotoIcon,
  UserIconPhoto,
} from "./styles"

const ArtistPost = () => {
  const [isPostLiked, setIsPostLiked] = useState(false)
  const [isCommentsSectionOpen, setIsCommentsSectionOpen] = useState(false)

  const onNotLikedPhotoClickHandler = () => {
    const notLikedButton = document.getElementById("notLikedButton")
    notLikedButton.style.transform = "scale(0.7)"
    setTimeout(() => {
      setIsPostLiked(true)
    }, 100)
  }

  const onLikedPhotoClickHandler = () => {
    const likedButton = document.getElementById("likedButton")
    likedButton.style.transform = "scale(0.8)"
    setTimeout(() => {
      setIsPostLiked(false)
    }, 100)
  }

  const isTablet = useIsTablet()

  const { isOpen, onOpen, onClose } = useDisclosure()

  return (
    <>
      <ArtistPostWrapper>
        <ArtistPostMainPhoto>
          <ArtistPostMainPhotoTitle>
            <Heading color={theme.colors.primary} level={4}>
              Flare Boom
            </Heading>
          </ArtistPostMainPhotoTitle>
        </ArtistPostMainPhoto>
        <ArtistPostContent>
          <ArtistPostLikesAndCommentsWrapper>
            <LikesContainer>
              {isPostLiked ? (
                <LikedPhotoIcon
                  id="likedButton"
                  onClick={onLikedPhotoClickHandler}
                />
              ) : (
                <NotLikedPhotoIcon
                  id="notLikedButton"
                  onClick={onNotLikedPhotoClickHandler}
                />
              )}
              <LikesNumber>
                <Paragraph color={theme.colors.background4} level={2}>
                  234 Likes
                </Paragraph>
              </LikesNumber>
            </LikesContainer>
            <CommentsContainer>
              {isCommentsSectionOpen ? (
                <CommentSectionClickedIcon
                  onClick={() => {
                    setIsCommentsSectionOpen(false)
                  }}
                />
              ) : (
                <CommentSectionNotClickedIcon onClick={onOpen} />
              )}
              <CommentsNumber>
                <Paragraph color={theme.colors.background4} level={2}>
                  234 Comments
                </Paragraph>
              </CommentsNumber>
            </CommentsContainer>
          </ArtistPostLikesAndCommentsWrapper>
          <ArtistPostUserWrapper>
            <UserIconPhoto />
            <Paragraph strong color={theme.colors.primary} level={2}>
              Jacob Vin
            </Paragraph>
          </ArtistPostUserWrapper>
          <ArtistPostDescription>
            <Paragraph level={2}>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua.
              Consequat ac felis donec et odio pellentesque diam volutpat
              commodo. Venenatis cras sedfelis eget. Quis hendrerit dolor magna
              eget est lorem ipsum.
            </Paragraph>
          </ArtistPostDescription>
        </ArtistPostContent>
      </ArtistPostWrapper>
      {!isTablet && (
        <Modal isCentered isOpen={isOpen} size="xxl" onClose={onClose}>
          <ModalOverlay />
          <ModalContent marginBottom="10px" marginTop="10px" width="735px">
            <ArtistPostCommentView onClose={onClose} />
          </ModalContent>
        </Modal>
      )}
      {isTablet && (
        <Drawer isOpen={isOpen} placement="right" size="full" onClose={onClose}>
          <DrawerOverlay />
          <DrawerContent
            backgroundColor="rgba(21, 21, 21, 0.8)"
            display="flex"
            justifyContent="center"
          >
            <ArtistPostCommentView onClose={onClose} />
          </DrawerContent>
        </Drawer>
      )}
    </>
  )
}

export default ArtistPost
