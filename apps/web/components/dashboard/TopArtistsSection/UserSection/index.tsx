import {
  UserSectionAreaWrapper,
  UserName,
  UserPhoto,
  UserIcon,
  UserIconStatus,
} from "./styles"

const UserSection = () => (
  <UserSectionAreaWrapper>
    <UserIcon>
      <UserPhoto />
      <UserIconStatus />
    </UserIcon>
    <UserName>Richard Brewl</UserName>
  </UserSectionAreaWrapper>
)

export default UserSection
