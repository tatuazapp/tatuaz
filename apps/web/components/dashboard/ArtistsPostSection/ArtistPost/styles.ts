import { BalloonHeart } from "@styled-icons/bootstrap/BalloonHeart"
import { BalloonHeartFill } from "@styled-icons/bootstrap/BalloonHeartFill"
import { Comment as CommentRegular } from "@styled-icons/boxicons-regular/Comment"
import { Comment as CommentSolid } from "@styled-icons/boxicons-solid/Comment"
import { down } from "styled-breakpoints"
import styled from "styled-components"
import { USER_CONTENT_WIDTH } from "../../profile/BackgroundAndAvatarContainer/styles"

export const ArtistPostWrapper = styled.div`
  width: 100%;
  max-width: ${USER_CONTENT_WIDTH - 32}px;
  background-color: ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.medium};
`

export const ArtistPostMainPhotos = styled.div`
  position: relative;

  overflow: hidden;
  display: grid;
  grid-gap: 0;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));

  min-height: 300px;
  max-height: 400px;

  border-top-left-radius: ${({ theme }) => theme.radius.medium};
  border-top-right-radius: ${({ theme }) => theme.radius.medium};

  img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }

  ${down("md")} {
    grid-template-columns: repeat(auto-fit, minmax(100px, 1fr));
    min-height: 150px;
    max-height: 250px;
  }
`

export const ArtistPostMainPhotoTitle = styled.p`
  position: absolute;
  bottom: ${({ theme }) => theme.space.xsmall};
  left: ${({ theme }) => theme.space.xsmall};

  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.primary};
`

export const ArtistPostContent = styled.div`
  padding-top: ${({ theme }) => theme.space.xsmall};
  padding-right: ${({ theme }) => theme.space.xsmall};
  padding-bottom: ${({ theme }) => theme.space.small};
  padding-left: ${({ theme }) => theme.space.xsmall};
`

export const ArtistPostLikesAndCommentsWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  margin-top: ${({ theme }) => theme.space.small};
`

export const LikesContainer = styled.div`
  display: flex;
  align-items: center;
`

export const NotLikedPhotoIcon = styled(BalloonHeart)`
  cursor: pointer;
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};
  transition: all ${({ theme }) => theme.animationTime.xxxxfast} ease-in-out;
`

export const LikedPhotoIcon = styled(BalloonHeartFill)`
  cursor: pointer;

  height: ${({ theme }) => theme.space.xlarge};

  color: ${({ theme }) => theme.colors.primary};

  transition: all ${({ theme }) => theme.animationTime.xxxxfast} ease-in-out;
  animation: ${({ theme }) => theme.animationTime.xxxfast} ease-out
    slideInFromLeft;

  @keyframes slideInFromLeft {
    0% {
      transform: scale(0.8);
    }
    100% {
      transform: scale(1);
    }
  }
`

export const LikesNumber = styled.p`
  margin-left: ${({ theme }) => theme.space.xxsmall};
  font-size: ${({ theme }) => theme.sizes.small};
  color: ${({ theme }) => theme.colors.background4};
`

export const CommentsContainer = styled.div`
  display: flex;
  align-items: center;
`

export const CommentSectionClickedIcon = styled(CommentSolid)`
  cursor: pointer;
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.background4};
`

export const CommentSectionNotClickedIcon = styled(CommentRegular)`
  cursor: pointer;
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.background4};
`

export const CommentsNumber = styled.p`
  margin-left: ${({ theme }) => theme.space.xxsmall};
  font-size: ${({ theme }) => theme.sizes.small};
  color: ${({ theme }) => theme.colors.background4};
`

export const ArtistPostUserWrapper = styled.div`
  display: flex;
  justify-content: start;
  padding-top: ${({ theme }) => theme.space.xsmall};
`

export const UserIconPhoto = styled.div<{
  photoUrl: string
}>`
  display: inline-block;

  width: ${({ theme }) => theme.space.large};
  height: ${({ theme }) => theme.space.large};
  margin-right: ${({ theme }) => theme.space.xsmall};
  margin-left: ${({ theme }) => theme.space.xxxsmall};

  background-image: url(${({ photoUrl }) => photoUrl});
  background-size: cover;
  border-radius: 50%;
`

export const UserName = styled.p`
  font-size: ${({ theme }) => theme.sizes.small};
  font-weight: 700;
  color: ${({ theme }) => theme.colors.primary};
`

export const ArtistPostDescription = styled.div`
  padding-top: ${({ theme }) => theme.space.xsmall};
  color: ${({ theme }) => theme.colors.secondary};
  text-align: justify;
`
