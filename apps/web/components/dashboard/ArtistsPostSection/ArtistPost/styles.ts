import { BalloonHeart } from "@styled-icons/bootstrap/BalloonHeart"
import { BalloonHeartFill } from "@styled-icons/bootstrap/BalloonHeartFill"
import { Comment as CommentRegular } from "@styled-icons/boxicons-regular/Comment"
import { Comment as CommentSolid } from "@styled-icons/boxicons-solid/Comment"
import styled from "styled-components"

export const ArtistPostWrapper = styled.div`
  /* width: 735px; */
  /* min-width: 2p50x; */
  /* min-width: 735px; */
  max-width: 735px;
  margin-bottom: ${({ theme }) => theme.space.xlarge};

  background-color: ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.medium};
`

export const ArtistPostMainPhoto = styled.div`
  position: relative;

  width: 100%;
  height: 300px;

  background-image: url("https://t3.ftcdn.net/jpg/01/01/05/24/360_F_101052491_D8WlkJsZclF5kO8LsA7AstXI9Ir4iuFl.jpg");
  background-size: cover;
  border-top-left-radius: ${({ theme }) => theme.radius.medium};
  border-top-right-radius: ${({ theme }) => theme.radius.medium};
`

export const ArtistPostMainPhotoTitle = styled.p`
  position: absolute;
  bottom: 12px;
  left: 12px;

  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.primary};
`

export const ArtistPostContent = styled.div`
  padding-top: ${({ theme }) => theme.space.xsmall};
  padding-right: ${({ theme }) => theme.space.xsmall};
  padding-bottom: ${({ theme }) => theme.space.xlarge};
  padding-left: ${({ theme }) => theme.space.xsmall};
`

export const ArtistPostLikesAndCommentsWrapper = styled.div`
  display: flex;
  justify-content: space-between;
`

export const LikesContainer = styled.div`
  display: flex;
  align-items: center;
`

export const NotLikedPhotoIcon = styled(BalloonHeart)`
  height: 34px;
  color: ${({ theme }) => theme.colors.primary};
  transition: all 0.1s ease-in-out;

  :hover {
    cursor: pointer;
  }
`

export const LikedPhotoIcon = styled(BalloonHeartFill)`
  height: 34px;
  color: ${({ theme }) => theme.colors.primary};
  transition: all 0.1s ease-in-out;
  animation: 0.1s ease-out slideInFromLeft;

  @keyframes slideInFromLeft {
    0% {
      transform: scale(0.7);
    }
    100% {
      transform: scale(1);
    }
  }

  :hover {
    cursor: pointer;
  }
`

export const LikesNumber = styled.p`
  margin-left: ${({ theme }) => theme.space.xxsmall};
  font-size: ${({ theme }) => theme.sizes.small};
  color: ${({ theme }) => theme.colors.background3};
`

export const CommentsContainer = styled.div`
  display: flex;
  align-items: center;
`

export const CommentSectionClickedIcon = styled(CommentSolid)`
  height: 34px;
  color: ${({ theme }) => theme.colors.background3};
  :hover {
    cursor: pointer;
  }
`

export const CommentSectionNotClickedIcon = styled(CommentRegular)`
  height: 34px;
  color: ${({ theme }) => theme.colors.background3};
  :hover {
    cursor: pointer;
  }
`

export const CommentsNumber = styled.p`
  margin-left: ${({ theme }) => theme.space.xxsmall};
  font-size: ${({ theme }) => theme.sizes.small};
  color: ${({ theme }) => theme.colors.background3};
`

export const ArtistPostUserWrapper = styled.div`
  display: flex;
  justify-content: start;
  padding-top: ${({ theme }) => theme.space.xsmall};
`

export const UserIconPhoto = styled.div`
  display: inline-block;

  width: 24px;
  height: 24px;
  margin-right: ${({ theme }) => theme.space.xsmall};
  margin-left: ${({ theme }) => theme.space.xxxsmall};

  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;

  /* background-color: #bbb; */
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
