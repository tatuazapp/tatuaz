import { Heading, Paragraph } from "@tatuaz/ui"
import { useState } from "react"
import { theme } from "../../../../../styles/theme"
import {
  ArtistPostCommentsViewHeader,
  ArtistPostCommentsViewHeaderCloseButton,
  ArtistPostContent,
  ArtistPostDescription,
  ArtistPostLikesAndCommentsWrapper,
  ArtistPostMainPhoto,
  ArtistPostMainPhotoTitle,
  ArtistPostUserWrapper,
  ArtistPostCommentsViewWrapper,
  CommentsContainer,
  CommentSectionClickedIcon,
  CommentsNumber,
  LikedPhotoIcon,
  LikesContainer,
  LikesNumber,
  NotLikedPhotoIcon,
  UserIconPhoto,
  ArtistPostCommentsViewCreateCommentSection,
  ArtistPostCommentsViewCreateCommentAvatar,
  ArtistPostCommentsViewCreateCommentInput,
  ArtistPostCommentsViewCommentsSection,
  ArtistPostCommentsViewDivider,
  CommentOwnerAvatar,
  ArtistPostCommentsViewCommentWrapper,
  CommentContent,
  CommentContentWrapper,
  CommentOptionsWrapper,
  CommentOption,
  CommentReactions,
  ReactionIcon,
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

  return (
    <ArtistPostCommentsViewWrapper>
      <ArtistPostCommentsViewHeader>
        <Heading color={theme.colors.secondary} level={4}>
          Jacob Vin Post
        </Heading>
        <ArtistPostCommentsViewHeaderCloseButton onClick={onClose} />
      </ArtistPostCommentsViewHeader>
      <ArtistPostScrollingArea>
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
              <CommentSectionClickedIcon />
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
          <ArtistPostCommentsViewDivider />
          <ArtistPostCommentsViewCommentsSection>
            <ArtistPostCommentsViewCommentWrapper>
              <CommentOwnerAvatar />
              <CommentContentWrapper>
                <CommentContent>
                  <Paragraph level={2}>
                    Besides the goals and assists, all Madrid players won it.
                    Vinicius was good. Winning with country matters the most
                    because then you dont assemble best players across the world
                    eget est lorem ipsum.
                  </Paragraph>
                  <CommentReactions>
                    <ReactionIcon />
                    <Paragraph level={2}>12</Paragraph>
                  </CommentReactions>
                </CommentContent>

                <CommentOptionsWrapper>
                  <CommentOption>
                    <Paragraph level={2}>Like</Paragraph>
                  </CommentOption>
                  <CommentOption>
                    <Paragraph level={2}>Reply</Paragraph>
                  </CommentOption>
                  <Paragraph color={theme.colors.background4} level={3}>
                    Today
                  </Paragraph>
                </CommentOptionsWrapper>
              </CommentContentWrapper>
            </ArtistPostCommentsViewCommentWrapper>
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
