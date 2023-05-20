import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  Button,
  Box,
  useToast,
} from "@chakra-ui/react"
import { X } from "@styled-icons/bootstrap/X"
import { useMutation } from "@tanstack/react-query"
import Image from "next/image"
import { FunctionComponent, useCallback, useMemo, useState } from "react"
import { useForm } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import { queryClient } from "../../../../pages/_app"
import Dropzone from "../../../common/Dropzone"
import useModals from "../../../common/modals/useModals"

type AvatarUploadModalProps = {
  isOpen: boolean
  onClose: () => void
}

const AvatarUploadModal: FunctionComponent<AvatarUploadModalProps> = ({
  isOpen,
  onClose,
}) => {
  const [uploadedImage, setUploadedImage] = useState<File | null>(null)

  const intl = useIntl()
  const toast = useToast()

  const {
    handleSubmit,
    setError,
    formState: { isSubmitting },
  } = useForm()

  const onFileAccepted = (file: File) => {
    setUploadedImage(file)
  }

  const removeUploadedImage = () => {
    setUploadedImage(null)
  }

  const uploadedImageAsBase64 = useMemo(
    () => (uploadedImage ? URL.createObjectURL(uploadedImage) : null),
    [uploadedImage]
  )

  const { confirm } = useModals()

  const mutation = useMutation({
    mutationFn: (data: { photo?: File }) =>
      api.identity.setForegroundPhoto(data),
    onError: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Wystąpił błąd podczas zmiany avataru",
          id: "24ACHW",
        }),
        status: "error",
        position: "top",
      })
    },
    onSuccess: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Zdjęcie profilowe zostało zmienione",
          id: "+oLkQK",
        }),
        status: "success",
        position: "top",
      })

      queryClient.invalidateQueries({
        queryKey: [queryKeys.whoAmI],
      })
    },
  })

  const deleteMutation = useMutation({
    mutationFn: () => api.identity.deleteForegroundPhoto({}),
    onError: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Wystąpił błąd podczas usuwania avataru",
          id: "l6acOX",
        }),
        status: "error",
        position: "top",
      })
    },
    onSuccess: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Avatar został usunięty",
          id: "L0sfbI",
        }),
        status: "success",
        position: "top",
      })

      queryClient.invalidateQueries({
        queryKey: [queryKeys.whoAmI],
      })
    },
  })

  const onSubmit = useMemo(
    () =>
      handleSubmit(async () => {
        if (!uploadedImage) {
          setError("backgroundPhoto", {
            message: intl.formatMessage({
              defaultMessage: "Avatar jest wymagany",
              id: "uwVZTo",
            }),
          })
          return
        }

        const formData = new FormData()
        formData.append("backgroundPhoto", uploadedImage)

        await mutation.mutateAsync({
          photo: uploadedImage,
        })

        onClose()
      }),
    [handleSubmit, intl, mutation, onClose, setError, uploadedImage]
  )

  const onDelete = useCallback(async () => {
    if (
      await confirm(
        intl.formatMessage({
          defaultMessage: "Czy na pewno chcesz usunąć avatar?",
          id: "YJAba8",
        })
      )
    ) {
      await deleteMutation.mutateAsync()
    }
  }, [confirm, deleteMutation, intl])

  return (
    <Modal isOpen={isOpen} size="xl" onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          <FormattedMessage defaultMessage="Zmień avatar" id="H4D1OM" />
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          {uploadedImageAsBase64 ? (
            <Box position="relative">
              <Image
                alt={intl.formatMessage({
                  defaultMessage: "Zdjęcie tła",
                  id: "cWa16k",
                })}
                height={0}
                sizes="100vw"
                src={uploadedImageAsBase64}
                style={{ width: "100%", height: "auto" }} // optional
                width={0}
              />
              <Button
                bgColor="blackAlpha.600"
                padding={0}
                position="absolute"
                right="0"
                top="0"
                onClick={removeUploadedImage}
              >
                <X size="24" />
              </Button>
            </Box>
          ) : (
            <Dropzone onFileAccepted={onFileAccepted} />
          )}
        </ModalBody>

        <ModalFooter>
          <Button
            colorScheme="red"
            isLoading={deleteMutation.isLoading}
            ml={3}
            variant="outline"
            onClick={onDelete}
          >
            <FormattedMessage
              defaultMessage="Usuń aktualny avatar"
              id="fAV7n8"
            />
          </Button>
          <Button mr={3} variant="ghost" onClick={onClose}>
            <FormattedMessage defaultMessage="Anuluj" id="JmR+Nv" />
          </Button>
          <Button
            colorScheme="primary"
            isLoading={isSubmitting}
            onClick={onSubmit}
          >
            <FormattedMessage defaultMessage="Zapisz" id="Jt6oMg" />
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  )
}

export default AvatarUploadModal
