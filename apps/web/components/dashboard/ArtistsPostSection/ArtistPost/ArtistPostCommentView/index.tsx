import { useState } from "react"
import {
  ArtistPostCommentsViewHeader,
  ArtistPostCommentsViewHeaderCloseButton,
  ArtistPostCommentsViewHeaderTitle,
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
  UserName,
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
  CommentPostDate,
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

  return (
    <ArtistPostCommentsViewWrapper>
      <ArtistPostCommentsViewHeader>
        <ArtistPostCommentsViewHeaderTitle>
          Jacob Vin Post
        </ArtistPostCommentsViewHeaderTitle>
        <ArtistPostCommentsViewHeaderCloseButton onClick={onClose} />
      </ArtistPostCommentsViewHeader>
      <ArtistPostScrollingArea>
        <ArtistPostMainPhoto>
          <ArtistPostMainPhotoTitle>Flare Boom</ArtistPostMainPhotoTitle>
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
              <LikesNumber>234 Likes</LikesNumber>
            </LikesContainer>
            <CommentsContainer>
              <CommentSectionClickedIcon />
              <CommentsNumber>234 Comments</CommentsNumber>
            </CommentsContainer>
          </ArtistPostLikesAndCommentsWrapper>
          <ArtistPostUserWrapper>
            <UserIconPhoto />
            <UserName>Jacob Vin</UserName>
          </ArtistPostUserWrapper>
          <ArtistPostDescription>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
            Consequat ac felis donec et odio pellentesque diam volutpat commodo.
            Venenatis cras sedfelis eget. Quis hendrerit dolor magna eget est
            lorem ipsum.
          </ArtistPostDescription>
          <ArtistPostCommentsViewDivider />
          <ArtistPostCommentsViewCommentsSection>
            <ArtistPostCommentsViewCommentWrapper>
              <CommentOwnerAvatar />
              <CommentContentWrapper>
                <CommentContent>
                  Besides the goals and assists, all Madrid players won it.
                  Vinicius was good. Winning with country matters the most
                  because then you dont assemble best players across the world
                  <CommentReactions>
                    <ReactionIcon />
                    <p>12</p>
                  </CommentReactions>
                </CommentContent>

                <CommentOptionsWrapper>
                  <CommentOption>Like</CommentOption>
                  <CommentOption>Reply</CommentOption>
                  <CommentPostDate>Today</CommentPostDate>
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
