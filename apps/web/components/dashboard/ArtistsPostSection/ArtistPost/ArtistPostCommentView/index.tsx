import { Heading, Paragraph } from "@tatuaz/ui"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../../styles/theme"
import {
  ArtistPostDescription,
  ArtistPostLikesAndCommentsWrapper,
  ArtistPostMainPhotos,
  ArtistPostMainPhotoTitle,
  CommentSectionClickedIcon,
  CommentsNumber,
  LikedPhotoIcon,
  LikesContainer,
  LikesNumber,
  NotLikedPhotoIcon,
  UserIconPhoto,
} from "../styles"
import PostComment from "./PostComment"
import {
  ArtistPostCommentsViewHeader,
  ArtistPostCommentsViewHeaderCloseButton,
  ArtistPostContent,
  ArtistPostUserWrapper,
  ArtistPostCommentsViewWrapper,
  CommentsContainer,
  ArtistPostCommentsViewCreateCommentSection,
  ArtistPostCommentsViewCreateCommentAvatar,
  ArtistPostCommentsViewCreateCommentInput,
  ArtistPostCommentsViewCommentsSection,
  ArtistPostCommentsViewDivider,
  ArtistPostScrollingArea,
} from "./styles"

type ArtistPostCommentViewProps = {
  onClose: () => void
}

const ArtistPostCommentView: React.FunctionComponent<
  ArtistPostCommentViewProps
> = ({ onClose }) => {
  const [isPostLiked, setIsPostLiked] = useState(false)

  const onNotLikedPhotoClickHandler = () => {
    const notLikedButton = document.getElementById("notLikedButton")
    if (!notLikedButton) {
      return
    }

    notLikedButton.style.transform = "scale(0.7)"
    setTimeout(() => {
      setIsPostLiked(true)
    }, 100)
  }

  const onLikedPhotoClickHandler = () => {
    const likedButton = document.getElementById("likedButton")
    if (!likedButton) {
      return
    }

    likedButton.style.transform = "scale(0.8)"
    setTimeout(() => {
      setIsPostLiked(false)
    }, 100)
  }

  return (
    <ArtistPostCommentsViewWrapper>
      <ArtistPostCommentsViewHeader>
        <Heading color={theme.colors.secondary} level={4}>
          Jacob Vin Post
        </Heading>
        <ArtistPostCommentsViewHeaderCloseButton onClick={onClose} />
      </ArtistPostCommentsViewHeader>
      <ArtistPostScrollingArea>
        <ArtistPostMainPhotos>
          <ArtistPostMainPhotoTitle>
            <Heading color={theme.colors.primary} level={4}>
              Flare Boom
            </Heading>
          </ArtistPostMainPhotoTitle>
        </ArtistPostMainPhotos>
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
                  234 <FormattedMessage defaultMessage="polubień" id="1/6yup" />
                </Paragraph>
              </LikesNumber>
            </LikesContainer>
            <CommentsContainer>
              <CommentSectionClickedIcon />
              <CommentsNumber>
                <Paragraph color={theme.colors.background4} level={2}>
                  234{" "}
                  <FormattedMessage defaultMessage="komentarzy" id="M9FmsT" />
                </Paragraph>
              </CommentsNumber>
            </CommentsContainer>
          </ArtistPostLikesAndCommentsWrapper>
          <ArtistPostUserWrapper>
            {/* TODO: Chnage it to sth dynamic */}
            <UserIconPhoto photoUrl="https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg" />
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
          <ArtistPostCommentsViewDivider />
          <ArtistPostCommentsViewCommentsSection>
            <PostComment />
            <PostComment />
            <PostComment />
          </ArtistPostCommentsViewCommentsSection>
        </ArtistPostContent>
      </ArtistPostScrollingArea>
      <ArtistPostCommentsViewCreateCommentSection>
        <ArtistPostCommentsViewCreateCommentAvatar />
        <ArtistPostCommentsViewCreateCommentInput placeholder="Napisz komentarz..." />
      </ArtistPostCommentsViewCreateCommentSection>
    </ArtistPostCommentsViewWrapper>
  )
}

export default ArtistPostCommentView
