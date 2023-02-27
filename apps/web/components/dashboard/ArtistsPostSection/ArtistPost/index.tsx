import { useState } from "react"
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
  UserName,
} from "./styles"

const ArtistPost = () => {
  const tmp = "Kk"

  const [isPostLiked, setIsPostLiked] = useState(false)
  const isCommentSectionOpen = false
  return (
    <ArtistPostWrapper>
      <ArtistPostMainPhoto>
        <ArtistPostMainPhotoTitle>Flare Boom</ArtistPostMainPhotoTitle>
      </ArtistPostMainPhoto>
      <ArtistPostContent>
        <ArtistPostLikesAndCommentsWrapper>
          <LikesContainer>
            {isPostLiked ? (
              <LikedPhotoIcon
                onClick={() => {
                  setIsPostLiked(false)
                }}
              />
            ) : (
              <NotLikedPhotoIcon
                onClick={() => {
                  setIsPostLiked(true)
                }}
              />
            )}
            <LikesNumber>234 Likes</LikesNumber>
          </LikesContainer>

          <CommentsContainer>
            {isCommentSectionOpen ? (
              <CommentSectionClickedIcon />
            ) : (
              <CommentSectionNotClickedIcon />
            )}
            <CommentsNumber>234 Likes</CommentsNumber>
          </CommentsContainer>
        </ArtistPostLikesAndCommentsWrapper>
        <ArtistPostUserWrapper>
          <UserIconPhoto />
          <UserName>Jacob Vin</UserName>
        </ArtistPostUserWrapper>
        <ArtistPostDescription>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Consequat
          ac felis donec et odio pellentesque diam volutpat commodo. Venenatis
          cras sedfelis eget. Quis hendrerit dolor magna eget est lorem ipsum.
        </ArtistPostDescription>
      </ArtistPostContent>
    </ArtistPostWrapper>
  )
}

export default ArtistPost
