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
    <ArtistPostWrapper>
      <ArtistPostMainPhoto>
        <ArtistPostMainPhotoTitle>Flare Boom</ArtistPostMainPhotoTitle>
      </ArtistPostMainPhoto>
      <ArtistPostContent>
        <ArtistPostLikesAndCommentsWrapper>
          <LikesContainer>
            {isPostLiked ? (
              <LikedPhotoIcon
                id="likedButton"
                // onClick={() => {
                //   setIsPostLiked(false)
                // }}
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
            {isCommentsSectionOpen ? (
              <CommentSectionClickedIcon
                onClick={() => {
                  setIsCommentsSectionOpen(false)
                }}
              />
            ) : (
              <CommentSectionNotClickedIcon
                onClick={() => {
                  setIsCommentsSectionOpen(true)
                }}
              />
            )}
            <CommentsNumber>234 Comments</CommentsNumber>
          </CommentsContainer>
        </ArtistPostLikesAndCommentsWrapper>
        {/* <Button onClick={onToggle}>Click Me</Button>
        <Collapse in={isOpen} animateOpacity>
          <Box
            p="40px"
            color="white"
            mt="4"
            bg="teal.500"
            rounded="md"
            shadow="md"
          >
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
            Consequat ac felis donec et odio pellentesque diam volutpat commodo.
            Venenatis cras sed felis eget. Quis hendrerit dolor magna eget est
            lorem ipsum.
          </Box>
        </Collapse> */}
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
